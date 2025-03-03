namespace TestApp
{
    public class Rent
    {
        public User Rentee { get; set; }

        public bool CanReturn(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            
            if (user.IsAdmin)
                return true;

            if (Rentee == user)
                return true;

            return false;
        }

    }


    public class User
    {
        public bool IsAdmin { get; set; }
    }

}