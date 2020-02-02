using System.Collections.Generic;
using DSC.Core;

namespace DSC.Dialogue
{
    public struct DialogueEventData_GroupController<GroupController> : IDialogueEventData where GroupController : BaseComponentGroupController
    {
        public List<GroupController> m_lstGroupController;
    }
}