using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Query;

namespace HSBC.CME.Plugins
{
    public class AccountPostUpdate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)

        {

            // Extract the tracing service for use in debugging sandboxed plug-ins.  
            // If you are not registering the plug-in in the sandbox, then you do  
            // not have to add any tracing service related code.  

            ITracingService tracingService =

                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.  

            IPluginExecutionContext context = (IPluginExecutionContext)

                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Obtain the organization service reference which you will need for  

            // web service calls.  

            IOrganizationServiceFactory serviceFactory =

                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);


            // The InputParameters collection contains all the data passed in the message request.  

            if (context.InputParameters.Contains("Target") &&

                context.InputParameters["Target"] is Entity)

            {

                // Obtain the target entity from the input parameters.  

                Entity accountRecord = (Entity)context.InputParameters["Target"];

                try
                {
                    string line1 = string.Empty;
                    string line2 = string.Empty;
                    string city = string.Empty;

                    if (accountRecord.Attributes.Contains("Address1_line1"))
                    {
                        // user is updating address1 line 1
                        line1 = accountRecord.Attributes["address1_line1"].ToString();
                    }
                    else
                    {
                        // user is not updating the address1 line1 value
                        // read the preimage 
                        Entity accountImage = (Entity)context.PreEntityImages["PreImage"];
                        line1 = accountImage.Attributes["address1_line1"].ToString();
                    }


                    if (accountRecord.Attributes.Contains("Address1_line2"))
                    {
                        line2 = accountRecord.Attributes["address1_line2"].ToString();
                    }
                    if (accountRecord.Attributes.Contains("Address1_city"))
                    {
                        city = accountRecord.Attributes["address1_city"].ToString();
                    }
                    //there are many ways we can pull data by using organization service
                    //1. QueryExpression
                    //2. QueryByAttribute
                    //3. FetchXML
                    //4. LINQ

                    //QueryExpression
                    // SQL : select * from contact where parentaccountid = accountRecord.Id
                    QueryExpression query = new QueryExpression();
                    query.EntityName = "contact";
                    query.ColumnSet.AddColumn("firstname");
                    query.Criteria.AddCondition("parentcustomerid", ConditionOperator.Equal, accountRecord.Id);

                    EntityCollection collection = service.RetrieveMultiple(query);

                    foreach(Entity contact in collection.Entities)
                    {
                        contact.Attributes.Add("address1_line1", line1);
                        contact.Attributes.Add("address1_line2", line2);
                        contact.Attributes.Add("address1_city", city);

                        service.Update(contact);
                    }

                }

                catch (FaultException<OrganizationServiceFault> ex)
                {

                    throw new InvalidPluginExecutionException("An error occurred in MyPlug-in.", ex);

                }

                catch (Exception ex)
                {

                    tracingService.Trace("MyPlugin: {0}", ex.ToString());

                    throw;

                }

            }

        }
    }
}
