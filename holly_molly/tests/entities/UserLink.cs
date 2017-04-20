namespace tests.entities
{
    public class UserLink : Link
    {
        public UserLink() {
        }

        public UserLink(User user1, User user2) {
            Entity1 = user1;
            Entity2 = user2;
        }

        public override Entity Entity1 { get; set; }
        public override Entity Entity2 { get; set; }
    }
}