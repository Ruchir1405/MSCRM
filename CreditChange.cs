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
    public class CreditChanges : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)

        {
            //plugin to have a Current profile changed to Gold if the credit limit changes > 10%
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

                Entity contactRecord = (Entity)context.InputParameters["Target"];

                try
                {
                    //get new value from Target or primry entity 
                    decimal newLimit = ((Money)contactRecord.Attributes["creditlimit"]).Value;

                    


                    //Get old value from Image
                    Entity imageRecord = ((Entity)context.PreEntityImages["PreImage"]);
                    

                    if(!imageRecord.Attributes.Contains("creditlimit"))
                    { return; }

                    decimal oldLimit = ((Money)imageRecord.Attributes["creditlimit"]).Value;
                    if (oldLimit == 0)
                    {
                        return;
                    }

                    decimal hike = (newLimit - oldLimit) * 100 / oldLimit;

                    if (hike >= 10)
                    {
                        contactRecord.Attributes.Add("description", "Credit limit has increased by " + hike.ToString() + "%");

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

