using DSC.Core;

namespace DSC.UI
{
    public abstract class BaseUIGroupController : BaseComponentGroupController
    {
        public abstract int groupID { get; }
        public abstract UIType groupType { get; }
    }
}