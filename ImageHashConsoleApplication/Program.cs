namespace ImageHashConsoleApplication
{
    using System;
    using System.Globalization;
    using System.IO;
    using CoenM.ImageHash.HashAlgorithms;
    using CoenM.ImageHash;

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(
                    "One parameter is missing, action need. use: hash {imagePath} | compare {hashOne} {hashTwo}");

                return;
            }

            switch (args[0])
            {
                case "hash" when args[1] == null:
                    Console.WriteLine("One parameter is missing, filepath need.");
                    return;
                case "hash":
                {
                    var hash = HashImage(args[1]);

                    Console.WriteLine(hash);

                    return;
                }
                case "compare" when args[1] == null || args[2] == null:
                    Console.WriteLine("You must provide to images hash: compare {hashOne} {hashTwo}");
                    return;
                case "compare":
                {
                    var hashImageOne = ulong.Parse(args[1]);
                    var hashImageTwo = ulong.Parse(args[2]);

                    var compareResult = CompareImagesHash(hashImageOne, hashImageTwo);

                    Console.WriteLine(compareResult.ToString(CultureInfo.CurrentCulture));

                    return;
                }
                default:
                    Console.WriteLine("Command not found");
                    break;
            }
        }

        private static string HashImage(string path)
        {
            var imageHashing = new PerceptualHash();
            using var stream = File.OpenRead(path);
            var imageHash = imageHashing.Hash(stream);

            return imageHash.ToString();
        }

        private static double CompareImagesHash(ulong imageHashOne, ulong imageHashTwo)
        {
            return CompareHash.Similarity(imageHashOne, imageHashTwo);
        }
    }
}