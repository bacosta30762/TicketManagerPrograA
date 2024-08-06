namespace Web.ViewModels
{
    public class ManageRolesViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
    }
}
