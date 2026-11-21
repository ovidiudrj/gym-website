namespace GymWebApp.Models
{
    public class Subscription
    {
        public int Id { get; set;}
        public string UserId { get; set; } = "";
        public AppUser ?User { get; set; }
        public int MembershipPlanId { get; set; }
        public MembershipPlan? MembershipPlan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal PricePaid { get; set; }
    }
}
