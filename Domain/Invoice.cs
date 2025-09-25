namespace Domain
{
    public class Invoice
    {

        public int calculTotal(int[] prices)
        {
            int ret = 0;
            foreach (int p in prices)
            {
                ret += p;
            }
            return ret;
        }

    }
}
