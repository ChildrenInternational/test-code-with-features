using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CRMPlugin
{
    public class ValidateUserInputPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];

                try
                {
                    ValidateUserInput(entity, service);
                }
                catch (InvalidPluginExecutionException ex)
                {
                    throw new InvalidPluginExecutionException("Validation failed: " + ex.Message);
                }
            }
        }

        private void ValidateUserInput(Entity entity, IOrganizationService service)
        {
            if (entity.LogicalName != "contact")
                return;

            string firstName = entity.Contains("firstname") ? entity["firstname"].ToString() : string.Empty;
            string lastName = entity.Contains("lastname") ? entity["lastname"].ToString() : string.Empty;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidPluginExecutionException("First name and last name are required.");
            }

            if (!IsValidName(firstName) || !IsValidName(lastName))
            {
                throw new InvalidPluginExecutionException("Names must only contain alphabetic characters.");
            }

            if (!IsUniqueFullName(firstName, lastName, service))
            {
                throw new InvalidPluginExecutionException("A contact with the same full name already exists.");
            }
        }

        private bool IsValidName(string name)
        {
            foreach (char c in name)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsUniqueFullName(string firstName, string lastName, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("contact")
            {
                ColumnSet = new ColumnSet("firstname", "lastname"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("firstname", ConditionOperator.Equal, firstName),
                        new ConditionExpression("lastname", ConditionOperator.Equal, lastName)
                    }
                }
            };

            EntityCollection results = service.RetrieveMultiple(query);
            return results.Entities.Count == 0;
        }
    }
}
