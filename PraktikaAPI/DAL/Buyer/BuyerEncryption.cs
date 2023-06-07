using PraktikaAPI.Encryption;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class BuyerEncryption
    {
        public static Buyer Encrypt(Buyer buyer) 
        {
            byte[] buffer;

            buffer = SymmetricEncryptionUtility.EncryptData(buyer.Address);
            buyer.Address = Convert.ToBase64String(buffer);

            if (buyer.Individual != null)
            {
                buffer = SymmetricEncryptionUtility.EncryptData(buyer.Individual.FullName);
                buyer.Individual.FullName = Convert.ToBase64String(buffer);

                buffer = SymmetricEncryptionUtility.EncryptData(buyer.Individual.SeriesPassportNumber);
                buyer.Individual.SeriesPassportNumber = Convert.ToBase64String(buffer);

                buffer = SymmetricEncryptionUtility.EncryptData(buyer.Individual.Phone);
                buyer.Individual.Phone = Convert.ToBase64String(buffer);
            }
            else
            {
                buffer = SymmetricEncryptionUtility.EncryptData(buyer.LegalEntity.Organization);
                buyer.LegalEntity.Organization = Convert.ToBase64String(buffer);

                buffer = SymmetricEncryptionUtility.EncryptData(buyer.LegalEntity.CheckingAccount);
                buyer.LegalEntity.CheckingAccount = Convert.ToBase64String(buffer);

                buffer = SymmetricEncryptionUtility.EncryptData(buyer.LegalEntity.Bank);
                buyer.LegalEntity.Bank = Convert.ToBase64String(buffer);

                buffer = SymmetricEncryptionUtility.EncryptData(buyer.LegalEntity.CorrespondentAccount);
                buyer.LegalEntity.CorrespondentAccount = Convert.ToBase64String(buffer);

                buffer = SymmetricEncryptionUtility.EncryptData(buyer.LegalEntity.Bic);
                buyer.LegalEntity.Bic = Convert.ToBase64String(buffer);

                buffer = SymmetricEncryptionUtility.EncryptData(buyer.LegalEntity.Rrc);
                buyer.LegalEntity.Rrc = Convert.ToBase64String(buffer);

                buffer = SymmetricEncryptionUtility.EncryptData(buyer.LegalEntity.Tin);
                buyer.LegalEntity.Tin = Convert.ToBase64String(buffer);

                buffer = SymmetricEncryptionUtility.EncryptData(buyer.LegalEntity.Phone);
                buyer.LegalEntity.Phone = Convert.ToBase64String(buffer);
            }
            return buyer;
        }

        public static Buyer Decrypt(Buyer buyer)
        {
            byte[] buffer;

            buffer = Convert.FromBase64String(buyer.Address);
            buyer.Address = SymmetricEncryptionUtility.DecryptData(buffer);

            if (buyer.Individual != null)
            {
                buffer = Convert.FromBase64String(buyer.Individual.FullName);
                buyer.Individual.FullName = SymmetricEncryptionUtility.DecryptData(buffer);

                buffer = Convert.FromBase64String(buyer.Individual.SeriesPassportNumber);
                buyer.Individual.SeriesPassportNumber = SymmetricEncryptionUtility.DecryptData(buffer);

                buffer = Convert.FromBase64String(buyer.Individual.Phone);
                buyer.Individual.Phone = SymmetricEncryptionUtility.DecryptData(buffer);
            }
            else
            {
                buffer = Convert.FromBase64String(buyer.LegalEntity.Organization);
                buyer.LegalEntity.Organization = SymmetricEncryptionUtility.DecryptData(buffer);

                buffer = Convert.FromBase64String(buyer.LegalEntity.CheckingAccount);
                buyer.LegalEntity.CheckingAccount = SymmetricEncryptionUtility.DecryptData(buffer);

                buffer = Convert.FromBase64String(buyer.LegalEntity.Bank);
                buyer.LegalEntity.Bank = SymmetricEncryptionUtility.DecryptData(buffer);

                buffer = Convert.FromBase64String(buyer.LegalEntity.CorrespondentAccount);
                buyer.LegalEntity.CorrespondentAccount = SymmetricEncryptionUtility.DecryptData(buffer);

                buffer = Convert.FromBase64String(buyer.LegalEntity.Bic);
                buyer.LegalEntity.Bic = SymmetricEncryptionUtility.DecryptData(buffer);

                buffer = Convert.FromBase64String(buyer.LegalEntity.Rrc);
                buyer.LegalEntity.Rrc = SymmetricEncryptionUtility.DecryptData(buffer);

                buffer = Convert.FromBase64String(buyer.LegalEntity.Tin);
                buyer.LegalEntity.Tin = SymmetricEncryptionUtility.DecryptData(buffer);

                buffer = Convert.FromBase64String(buyer.LegalEntity.Phone);
                buyer.LegalEntity.Phone = SymmetricEncryptionUtility.DecryptData(buffer);
            }
            return buyer;
        }
    }
}
