using FirebaseAdmin.Auth;
using System.Threading.Tasks;

namespace RankList.Services
{
    public static class FirebaseTokenValidator
    {
        public static async Task<bool> ValidateFirebaseToken(string idToken)
        {
            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                // decodedToken.Uid contém o ID do usuário autenticado
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}