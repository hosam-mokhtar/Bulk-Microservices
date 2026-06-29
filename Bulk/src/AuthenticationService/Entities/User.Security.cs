namespace AuthenticationService.Entities
{
    public partial class User
    {
        public void UpdatePassword(string passwordHash, Guid updatedBy)
        {
            PasswordHash = passwordHash;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        public void VerifyEmail()
        {
            if (IsEmailVerified)
                return;

            IsEmailVerified = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
