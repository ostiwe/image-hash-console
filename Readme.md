### Requirements

* .NET runtime

### Important

I made this application for my tasks and for my system configuration. During the operation of this application, usage
high of RAM is possible

### Usage

Download binary from last release

Use command `./ImageHashConsoleApplication hash /path/to/image` for hash one image.

For hash all images in directory:

```shell
$ ./ImageHashConsoleApplication hash /path/to/image/directory
``` 

Result:

```shell
./ImageHashConsoleApplication hash ~/Downloads/imagesS
Calculated perceptual hash for file: /home/ostiwe/Downloads/imagesS/ishan-seefromthesky-DtWyp_4YEes-unsplash.jpg hash: 10333678333505845673
Calculated perceptual hash for file: /home/ostiwe/Downloads/imagesS/guille-pozzi-PO0UHx-5mHo-unsplash.jpg hash: 12992123694817588148
Calculated perceptual hash for file: /home/ostiwe/Downloads/imagesS/james-wheeler-XuAxyq0uRT0-unsplash.jpg hash: 17014150402375529576
Calculated perceptual hash for file: /home/ostiwe/Downloads/imagesS/max-larochelle-uu-Jw5SunYI-unsplash.jpg hash: 11111055159896272332
...
Calculated perceptual hash for file: /home/ostiwe/Downloads/imagesS/dominik-fischer-f9uAn6snTs8-unsplash.jpg hash: 15686150518179522201
Calculated perceptual hash for file: /home/ostiwe/Downloads/imagesS/thought-catalog-bGiMXk8sMHw-unsplash.jpg hash: 17361623836643998917
Calculated perceptual hash for file: /home/ostiwe/Downloads/imagesS/carles-rabada-yKAgTzz7jWk-unsplash.jpg hash: 17045220349039073432
Calculated perceptual hash for file: /home/ostiwe/Downloads/imagesS/simon-zhu-4hluhoRJokI-unsplash.jpg hash: 13875076465402953180
```

For hash all images in directory and return only hashes:

```shell
$ ./ImageHashConsoleApplication hash /path/to/image/directory --only-hash
``` 

Result:

```shell
$ ./ImageHashConsoleApplication hash ~/Downloads/imagesS --only-hash
17045220349039073432
12992123694817588148
15686150518179522201
14469252222504389657
16847159740603029929
...
17014150402375529576
10747145181797424755
```

For compare images:

```shell
$ ./ImageHashConsoleApplication compare 12992123694817588148 10747145181797424755
```

Result:

```shell
$ ./publish/ImageHashConsoleApplication compare 12992123694817588148 10747145181797424755
53.125
```