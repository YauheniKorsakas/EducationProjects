using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Structural.Composite
{
    public class CompositeCase : ICase
    {
        public async Task RunAsync() {
            // add new person into system
            var addNewPersonIntoSystem = new ActionNode("Add person into system");
            var validatePerson = new ActionNode("Validate person");
            var mapPersonIntoDto = new ActionNode("Map person into dto");
            var delegateSavingPersonFromWebToService = new ActionNode("Delegate saving from web to service");
            var validatePersonOnServiceLevel = new ActionNode("Validate person on service level");
            var delegateSavingFromServiceToRepository = new ActionNode("Delegate saving from service to repository");
            var validatePersonOnRepositoryLevel = new ActionNode("Validate person on repository level");
            var callDatabaseActionsToSavePerson = new ActionNode("Call database actions to save person");
            var returnSavedPersonBackAsResponce = new ActionNode("Return saved person back as responce");

            addNewPersonIntoSystem.AddChild(validatePerson);
            addNewPersonIntoSystem.AddChild(mapPersonIntoDto);

            mapPersonIntoDto.AddChild(delegateSavingPersonFromWebToService);

            delegateSavingPersonFromWebToService.AddChild(validatePersonOnServiceLevel);
            delegateSavingPersonFromWebToService.AddChild(delegateSavingFromServiceToRepository);

            delegateSavingFromServiceToRepository.AddChild(validatePersonOnRepositoryLevel);
            delegateSavingFromServiceToRepository.AddChild(callDatabaseActionsToSavePerson);
            delegateSavingFromServiceToRepository.AddChild(returnSavedPersonBackAsResponce);

            addNewPersonIntoSystem.DisplayActionsToEnd();
        }

        public class ActionNode
        {
            public string ActionName { get; }
            protected List<ActionNode> children { get; } = new List<ActionNode>();

            public ActionNode(string actionName) {
                ActionName = actionName;
            }

            public void AddChild(ActionNode node) {
                children.Add(node);
            }

            public void DisplayActionsToEnd(int level = 1) {
                Console.WriteLine($"{ActionName} - {level}lvl");
                level++;

                foreach (var child in children) {
                    child.DisplayActionsToEnd(level);
                }
            }
        }
    }
}
