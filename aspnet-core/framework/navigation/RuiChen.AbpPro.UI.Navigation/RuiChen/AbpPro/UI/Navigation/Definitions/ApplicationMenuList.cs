namespace RuiChen.AbpPro.UI.Navigation
{
    public class ApplicationMenuList : List<ApplicationMenu>
    {
        public ApplicationMenuList() : base() { }

        public ApplicationMenuList(int capacity) : base(capacity) { }

        public ApplicationMenuList(IEnumerable<ApplicationMenu> collection) : base(collection) { }

        public void Normalize()
        {
            RemoveEmptyItems();
            Order();
        }

        private void RemoveEmptyItems()
        {
            RemoveAll(item => item.IsLeaf && string.IsNullOrEmpty(item.Url));
        }

        private void Order()
        {
            // 使用内置的 Sort 方法来提高性能
            Sort((x, y) => x.Order.CompareTo(y.Order));
        }
    }

}
