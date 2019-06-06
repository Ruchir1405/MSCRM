﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using System.ServiceModel;

namespace HSBC.CME.Plugins
{
    public class AccountPostCreate : IPlugin
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
                    tracingService.Trace("AccountPostCreate is started");

                         //create task for Account 
                        // Create task record 

                        Entity taskRecord = new Entity("task");

                        // String Datatype
                        taskRecord.Attributes.Add("subject", "Start KYC for Customer");
                        taskRecord.Attributes.Add("description", "Start KYC for Customer");

                        //date datatype
                        taskRecord.Attributes.Add("scheduledend", DateTime.Now.AddDays(3));

                        // lookup
                        taskRecord.Attributes.Add("regardingobjectid", new EntityReference("account", accountRecord.Id));

                        // OptionSet
                        // taskRecord.Attributes.Add("regardngobjectid", new EntityReference("account", accountRecord.Id);

                        service.Create(taskRecord);
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
