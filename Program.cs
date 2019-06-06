using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk;
using System.IO;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Early Bound Generator - wrapper class 
//            Console.WriteLine("Enter your credentials");
  //          string password = Console.ReadLine();
            string connectionString = "AuthType = Office365; Url = https://rkj84.crm8.dynamics.com;UserName=rkj@rkj84.onmicrosoft.com;Password =Advik007";
            CrmServiceClient service = new CrmServiceClient(connectionString);

            Contact newContact = new Contact();
            newContact.FirstName = "from Azure Webapp";
       //     newContact.LastName = "Cena";
            service.Create(newContact);

            // without keys
            //update John Smith (john@gmail.com)
            //QueryExpression query = new QueryExpression("contact");
            //query.ColumnSet.AddColumn("emailaddress1");
            //query.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, "john@gmail.com");
            //EntityCollection collection = service.RetrieveMultiple(query);
            //Guid guid = collection.Entities.FirstOrDefault().Id;

            //Entity contact1 = new Entity("contact", new Guid("GUID"));
            //contact1.Attributes.Add("description", "some desc");
            //service.Update(contact1);

            //with keys
            //Entity contact2 = new Entity("contact", "rkj_EmailDuplicateCheckKey", "john@gmail.com");
            //contact2.Attributes.Add("description", "some desc");
            //service.Update(contact2);



            // Calling Action from code
            // Late binding

            //OrganizationRequest req = new OrganizationRequest("rkj_LeadFollowUp");
            //req.Parameters.Add("DueDate", DateTime.Now.AddDays(5));

            // Early binding

            //Lead lead = new Lead();
            //lead.Subject = "New Enuiry";
            //lead.LastName = "Lastname";

            // if we dont want to use the library we call it the below way .......
            //Entity leadRecord = new Entity("leadRecord");
            //leadRecord.Attributes.Add("subject", "new enquiry");
            //leadRecord.Attributes.Add("lastname", "lastname");

            //Guid leadGuid = service.Create(lead);

            //req.Parameters.Add("Target", new EntityReference("lead", leadGuid));

            //OrganizationResponse response = service.Execute(req);

            //Console.Write(response.Results["Status"].ToString());

            //rkj_country country = new rkj_country();
            //country.rkj_name = "Nepal";
            //Guid countryGuid = service.Create(country);

            //Contact newContact = new Contact();
            //newContact.FirstName = "John";
            //newContact.LastName = "Cena";
            //newContact.rkj_CountryId = new EntityReference("rkj_country", countryGuid);

            //service.Create(newContact);


            // 3. Using Fetch XML

            //            string query = @"<fetch distinct='false' mapping='logical' output-format='xml-platform' version='1.0'>
            //<entity name = 'contact'>
            // <attribute name = 'fullname'/>
            //  <attribute name = 'parentcustomerid'/>
            //   <attribute name = 'telephone1'/>
            //    <attribute name = 'emailaddress1'/>
            //     <attribute name = 'contactid'/>
            //      <order descending = 'false' attribute = 'fullname'/>
            //         <filter type = 'and'>
            //          <condition attribute = 'ownerid' operator= 'eq-userid'/>
            //             <condition attribute = 'statecode' operator= 'eq' value = '0'/>
            //                 </filter>
            //                 </entity>
            //                 </fetch> ";

            //            EntityCollection collection = service.RetrieveMultiple(new FetchExpression(query));

            //            foreach(Entity contact in collection.Entities)
            //            {

            //                Console.WriteLine(contact.Attributes["fullname"].ToString());

            //            }

            //            string[] lines = File.ReadAllLines("C:\\Users\\admin\\Desktop\\ConsoleAppload.txt");

            //           // Entity contact = new Entity("contact");
            //           // contact.Attributes.Add("firstname", attributes[0]);
            //            //contact.Attributes.Add("lastname", attributes[1]);

            //            //CreateRequest req2 = new CreateRequest();
            //            //req2.Target = contact;

            //            //service.Execute(req2);

            //            ExecuteMultipleRequest multireq = new ExecuteMultipleRequest();
            //            multireq.Settings = new ExecuteMultipleSettings()
            //            {
            //                ContinueOnError = true,
            //                ReturnResponses = false

            //            };

            //            multireq.Requests = new OrganizationRequestCollection();

            //            CreateRequest req;
            //            foreach (string line in lines)
            //            {
            //                string[] attributes = line.Split(',');

            //                Entity contact = new Entity("contact");
            //                contact.Attributes.Add("firstname", attributes[0]);
            //                contact.Attributes.Add("lastname", attributes[1]);

            //                //construct request object
            //                req = new CreateRequest();
            //                req.Target = contact;
            //                multireq.Requests.Add(req);

            //                Guid guid = service.Create(contact);

            //                Console.WriteLine(guid);
            //            }
            //Console.Read();
        }
    }
}
