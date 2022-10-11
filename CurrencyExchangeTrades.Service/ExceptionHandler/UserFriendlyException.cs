namespace CurrencyExchangeTrades.Service.ExceptionHandler
{
    public class UserFriendlyException
    {
        public static string CatchException(Exception exception)
        {
            try
            {
                throw exception;
            }
            catch (NoValueException ex)
            {
                return "NoValueException - Please check the from currency: " + ex.Message;
            }
            catch (NoValidValueException ex)
            {
                return "NoValidValueException - Currency rate has a problem for currency: " + ex.Message;
            }
            catch (ReachedMaximumException ex)
            {
                return "ReachedMaximumException - Please wait an hour: " + ex.Message;
            }
            catch (NoSymbolException ex)
            {
                return "NoSymbolException - Please check the from/to currency: " + ex.Message;
            }
            catch (NoSuccessException ex)
            {
                return "NoSuccessException - Trade didn't save properly: " + ex.Message;
            }
            catch (InvalidCurrencyException ex)
            {
                return "InvalidCurrencyException - Please check the from/to currency: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Something gone wrong: " + ex.Message;
            }
        }
    }
}

