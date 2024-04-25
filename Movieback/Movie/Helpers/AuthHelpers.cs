//using Microsoft.IdentityModel.Tokens;
//using Movie.Models;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Movie.Helpers
//{
//	public class AuthHelpers
//	{
//		public string GenerateJWTToken(User user)
//		{
//			var claims = new List<Claim> {
//		new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//		new Claim(ClaimTypes.Name, user.Name),
//	};
//			var jwtToken = new JwtSecurityToken(
//				claims: claims,
//				notBefore: DateTime.UtcNow,
//				expires: DateTime.UtcNow.AddDays(30),
//				signingCredentials: new SigningCredentials(
//					new SymmetricSecurityKey(
//					   Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"])
//						),
//					SecurityAlgorithms.HmacSha256Signature)
//				);
//			return new JwtSecurityTokenHandler().WriteToken(jwtToken);
//		}
//	}
//}
