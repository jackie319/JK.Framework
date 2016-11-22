using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions
{
    public enum CustomerLoginResults
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Customer dies not exist (email or username)
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// Account have not been activated
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// Customer has been deleted 
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// Customer not registered 
        /// </summary>
        NotRegistered = 6,
    }

    //public virtual CustomerLoginResults ValidateCustomer(string usernameOrEmail, string password)
    //{
    //    var customer = _customerSettings.UsernamesEnabled ?
    //        _customerService.GetCustomerByUsername(usernameOrEmail) :
    //        _customerService.GetCustomerByEmail(usernameOrEmail);

    //    if (customer == null)
    //        return CustomerLoginResults.CustomerNotExist;
    //    if (customer.Deleted)
    //        return CustomerLoginResults.Deleted;
    //    if (!customer.Active)
    //        return CustomerLoginResults.NotActive;
    //    //only registered can login
    //    if (!customer.IsRegistered())
    //        return CustomerLoginResults.NotRegistered;

    //    string pwd;
    //    switch (customer.PasswordFormat)
    //    {
    //        case PasswordFormat.Encrypted:
    //            pwd = _encryptionService.EncryptText(password);
    //            break;
    //        case PasswordFormat.Hashed:
    //            pwd = _encryptionService.CreatePasswordHash(password, customer.PasswordSalt, _customerSettings.HashedPasswordFormat);
    //            break;
    //        default:
    //            pwd = password;
    //            break;
    //    }

    //    bool isValid = pwd == customer.Password;
    //    if (!isValid)
    //        return CustomerLoginResults.WrongPassword;

    //    //save last login date
    //    customer.LastLoginDateUtc = DateTime.UtcNow;
    //    _customerService.UpdateCustomer(customer);
    //    return CustomerLoginResults.Successful;
    //}
}
