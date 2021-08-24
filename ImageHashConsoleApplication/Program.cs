using System;
using System.Globalization;
using System.IO;
using System.Threading;
using CoenM.ImageHash;
using CoenM.ImageHash.HashAlgorithms;

namespace ImageHashConsoleApplication
{
    class Program
    {
        static Semaphore sem = new(25, 25);

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(
                    "One parameter is missing, action need. use: hash {imagePath} | hash {pathImagesDir} | compare {hashOne} {hashTwo}"
                );

                return;
            }

            switch (args[0])
            {
                case "hash" when args[1] == null:
                    Console.WriteLine("One parameter is missing, filepath need.");
                    return;
                case "hash":
                {
                    var pathAttribute = File.GetAttributes(args[1]);
                    if ((pathAttribute & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        var onlyHash = false;
                        if (args.Length > 2)
                        {
                            onlyHash = (args[2] != null & args[2] == "--only-hash");
                        }
                       
                        HashImagesInDirectory(args[1], onlyHash);

                        return;
                    }

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

        private static void HashImagesInDirectory(string pathToDirectory, bool onlyHashOption = false)
        {
            var filesToHash = Directory.GetFiles(pathToDirectory);
            var index = 0;
            var maxThreads = filesToHash.Length + 1;
            Thread[] threads = new Thread[maxThreads];
            foreach (var fileToHash in filesToHash)
            {
                var fileAttribute = File.GetAttributes(fileToHash);

                if ((fileAttribute & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Console.WriteLine(fileToHash + " is directory, skipping");

                    continue;
                }

                threads[index] = new Thread(() =>
                {
                    sem.WaitOne();
                    var hash = HashImage(fileToHash);
                    if (onlyHashOption)
                    {
                        Console.WriteLine(hash);
                    }
                    else
                    {
                        Console.WriteLine("Calculated perceptual hash for file: " + fileToHash + " hash: " + hash);
                    }

                    sem.Release();
                });
                threads[index].Start();
                index++;
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