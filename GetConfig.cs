using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk.Query;
using System.Activities;
using System.ServiceModel;


namespace HSBC.CME.CustomWorkflows
{
    public class GetConfig : CodeActivity
    {
        [Input("Enter Key Name")]
        public InArgument<string> Name {get; set;}

        [Output("Value")]
        public OutArgument<string> Value {get; set;}

        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();

            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            //custom code starts here

            try
            {
               string name = Name.Get(executionContext);

                string value = Helper.GetConfiguration(service, name);

                Value.Set(executionContext, value);
                
            }
            catch (FaultException<OrganizationServiceFault> e)
            {
                tracingService.Trace(e.Message);                               
            }

        }

        }
    }
