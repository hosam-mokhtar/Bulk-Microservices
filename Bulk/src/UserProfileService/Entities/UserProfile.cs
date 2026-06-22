namespace UserProfileService.Entities;

public sealed class UserProfile
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public bool IsPremiumCached { get; set; }
    public DateTime MemberSince { get; set; } = DateTime.UtcNow;

    public UserPreference Preference { get; set; } = null!;
    public NotificationSetting NotificationSetting { get; set; } = null!;
    public PrivacySetting PrivacySetting { get; set; } = null!;

    //private UserProfile() { }
}
