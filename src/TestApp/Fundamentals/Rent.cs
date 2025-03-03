namespace TestApp
{
    public class Rent
    {
        public User Rentee { get; set; }

        public bool CanReturn(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            return user.IsAdmin || Rentee == user;
        }

    }


    public class User
    {
        public bool IsAdmin { get; set; }
    }

}