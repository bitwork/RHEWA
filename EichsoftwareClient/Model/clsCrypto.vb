Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

''' <summary>
''' This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and
''' decrypt data. As long as encryption and decryption routines use the same
''' parameters to generate the keys, the keys are guaranteed to be the same.
''' The class uses static functions with duplicate code to make it easier to
''' demonstrate encryption and decryption logic. In a real-life application,
''' this may not be the most efficient way of handling encryption, so - as
''' soon as you feel comfortable with it - you may want to redesign this class.
''' </summary>
Public Class RijndaelSimple

    ''' <summary>
    ''' Encrypts specified plaintext using Rijndael symmetric key algorithm
    ''' and returns a base64-encoded result.
    ''' </summary>
    ''' <param name="plainText">
    ''' Plaintext value to be encrypted. </param>
    ''' <param name="passPhrase">
    ''' Passphrase from which a pseudo-random password will be derived. The
    ''' derived password will be used to generate the encryption key.
    ''' Passphrase can be any string. In this example we assume that this
    ''' passphrase is an ASCII string.</param>
    ''' <param name="saltValue">
    ''' Salt value used along with passphrase to generate password. Salt can
    ''' be any string. In this example we assume that salt is an ASCII string.</param>
    ''' <param name="hashAlgorithm">
    ''' Hash algorithm used to generate password. Allowed values are: "MD5" and
    ''' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.</param>
    ''' <param name="passwordIterations">
    ''' Number of iterations used to generate password. One or two iterations
    ''' should be enough.</param>
    ''' <param name="initVector">
    ''' Initialization vector (or IV). This value is required to encrypt the
    ''' first block of plaintext data. For RijndaelManaged class IV must be
    ''' exactly 16 ASCII characters long.</param>
    ''' <param name="keySize">
    ''' Size of encryption key in bits. Allowed values are: 128, 192, and 256.
    ''' Longer keys are more secure than shorter keys.</param>
    ''' <returns>
    ''' Encrypted value formatted as a base64-encoded string.
    ''' </returns>
    '''
    Public Shared Function Encrypt(plainText As String, passPhrase As String, saltValue As String, hashAlgorithm As String, passwordIterations As Integer, initVector As String,
        keySize As Integer) As string
        ' Convert strings into byte arrays.
        ' Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
        ' encoding.
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our plaintext into a byte array.
        ' Let us assume that plaintext contains UTF8-encoded characters.
        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)

        ' First, we must create a password, from which the key will be derived.
        ' This password will be generated from the specified passphrase and
        ' salt value. The password will be created using the specified hash
        ' algorithm. Password creation can be done in several iterations.
        '  Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
        Dim password As New Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As New RijndaelManaged()

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate encryptor from the existing key bytes and initialization
        ' vector. Key size will be defined based on the number of the key
        ' bytes.
        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As New MemoryStream()

        ' Define cryptographic stream (always use Write mode for encryption).
        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
        ' Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

        ' Finish encrypting.
        cryptoStream.FlushFinalBlock()

        ' Convert our encrypted data from a memory stream into a byte array.
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert encrypted data into a base64-encoded string.
        Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)

        ' Return encrypted string.
        Return cipherText
    End Function

    ''' <summary>
    ''' Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    ''' </summary>
    ''' <param name="cipherText">
    ''' Base64-formatted ciphertext value.
    ''' </param>
    ''' <param name="passPhrase">
    ''' Passphrase from which a pseudo-random password will be derived. The
    ''' derived password will be used to generate the encryption key.
    ''' Passphrase can be any string. In this example we assume that this
    ''' passphrase is an ASCII string.
    ''' </param>
    ''' <param name="saltValue">
    ''' Salt value used along with passphrase to generate password. Salt can
    ''' be any string. In this example we assume that salt is an ASCII string.
    ''' </param>
    ''' <param name="hashAlgorithm">
    ''' Hash algorithm used to generate password. Allowed values are: "MD5" and
    ''' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    ''' </param>
    ''' <param name="passwordIterations">
    ''' Number of iterations used to generate password. One or two iterations
    ''' should be enough.
    ''' </param>
    ''' <param name="initVector">
    ''' Initialization vector (or IV). This value is required to encrypt the
    ''' first block of plaintext data. For RijndaelManaged class IV must be
    ''' exactly 16 ASCII characters long.
    ''' </param>
    ''' <param name="keySize">
    ''' Size of encryption key in bits. Allowed values are: 128, 192, and 256.
    ''' Longer keys are more secure than shorter keys.
    ''' </param>
    ''' <returns>
    ''' Decrypted string value.
    ''' </returns>
    ''' <remarks>
    ''' Most of the logic in this function is similar to the Encrypt
    ''' logic. In order for decryption to work, all parameters of this function
    ''' - except cipherText value - must match the corresponding parameters of
    ''' the Encrypt function which was called to generate the
    ''' ciphertext.
    ''' </remarks>
    Public Shared Function Decrypt(cipherText As String, passPhrase As String, saltValue As String, hashAlgorithm As String, passwordIterations As Integer, initVector As String,
        keySize As Integer) As string
        ' Convert strings defining encryption key characteristics into byte
        ' arrays. Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
        ' encoding.
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our ciphertext into a byte array.
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)

        ' First, we must create a password, from which the key will be
        ' derived. This password will be generated from the specified
        ' passphrase and salt value. The password will be created using
        ' the specified hash algorithm. Password creation can be done in
        ' several iterations.
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
        ' Dim password As New Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As New RijndaelManaged()

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate decryptor from the existing key bytes and initialization
        ' vector. Key size will be defined based on the number of the key
        ' bytes.
        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As New MemoryStream(cipherTextBytes)

        ' Define cryptographic stream (always use Read mode for encryption).
        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)

        ' Since at this point we don't know what the size of decrypted data
        ' will be, allocate the buffer long enough to hold ciphertext;
        ' plaintext is never longer than ciphertext.
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}

        ' Start decrypting.
        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert decrypted data into a string.
        ' Let us assume that the original plaintext string was UTF8-encoded.
        Dim plainText As String = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

        ' Return decrypted string.
        Return plainText
    End Function
End Class

''' <summary>
''' Illustrates the use of RijndaelSimple class to encrypt and decrypt data.
''' </summary>
Public Class RijndaelSimpleTest
    Private Shared Sub Main(args As String())
        Dim plainText As String = "44M7hPLnAh9t5dp6S4eLLnkHPHLuMbEG"
        ' original plaintext
        Dim passPhrase As String = "Pas5pr@se"
        ' can be any string
        Dim saltValue As String = "s@1tValue"
        ' can be any string
        Dim hashAlgorithm As String = "SHA1"
        ' can be "MD5"
        Dim passwordIterations As Integer = 2
        ' can be any number
        Dim initVector As String = "@1B2c3D4e5F6g7H8"
        ' must be 16 bytes
        Dim keySize As Integer = 256
        ' can be 192 or 128

        Console.WriteLine([String].Format("Plaintext : {0}", plainText))

        Dim cipherText As String = RijndaelSimple.Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector,
            keySize)

        Console.WriteLine([String].Format("Encrypted : {0}", cipherText))

        plainText = RijndaelSimple.Decrypt(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector,
            keySize)

        Console.WriteLine([String].Format("Decrypted : {0}", plainText))
    End Sub
End Class