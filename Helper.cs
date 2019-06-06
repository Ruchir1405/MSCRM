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
    public class Helper
    {
        //Date retrive from service
        // 2nd Method, using QueryByAttribute
        public static string GetConfiguration(IOrganizationService service, string name)
        {
            QueryByAttribute query = new QueryByAttribute("rkj_config");
            query.AddAttributeValue("rkj_name", name);
            query.ColumnSet = new ColumnSet(new string[] { "rkj_value" });

            EntityCollection collection = service.RetrieveMultiple(query);

            string value = collection.Entities.FirstOrDefault().Attributes["rkj_value"].ToString();
            return value;

        }


    }
}
