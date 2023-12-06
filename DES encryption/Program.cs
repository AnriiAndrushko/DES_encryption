using System.Text;
using DES = DES_encryption.DES;
using System.Security.Cryptography;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Який режим роботи? режим шифрування\\режим створення цифрового підпису 1\\0");
bool answer = Console.ReadLine().Equals("1");


Console.WriteLine("Введіть назву файлу який треба зашифрувати:");
string fileName = Console.ReadLine();

Console.WriteLine("Введіть ключ:");
string key = Console.ReadLine();
string message;
byte[] encryptedMessage;

byte[] iv = new byte[8];

RandomNumberGenerator.Create().GetBytes(iv);

using (var nf = File.Exists(fileName + "Encrypted") ? File.OpenWrite(fileName + "Encrypted") : File.Create(fileName.Split('.')[0] + "Encrypted.txt"))
{
    message = File.ReadAllText(fileName);
    encryptedMessage = answer?DES.Encrypt(message, key):DES.EncryptCBC(message, key, iv);
    Console.WriteLine($"Зашифроване повідомлення: {Convert.ToHexString(encryptedMessage)}");
    nf.Write(encryptedMessage);
}
File.ReadAllBytes(fileName.Split('.')[0] + "Encrypted.txt");
var decryptedMessage = answer ? DES.Decrypt(encryptedMessage, key):DES.DecryptCBC(encryptedMessage, key, iv);
Console.WriteLine($"Розшифроване повідомлення: {decryptedMessage}");

Console.WriteLine(decryptedMessage.Equals(message) ? "Все вірно" : "Помилка");

