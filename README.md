# ImageSearep
Stands for "Image Search And Replace" this tool can be used to search for supported image types in a binary file and allows the user to replace them with images of the same type granted they are the same size or smaller than the original image they are replacing.  At this time only PNG images are supported.

### Note
Please do not use this application unless you've safely made a backup, this could irrevocably ruin the file you're working on and I take no responsibility for loss of data.  *Use at your own risk!!!*

## Status
The application is stable to the best of my knowledge and works great when replacing PNG images (in WPF application at least).

## Wishlist
If I decide to continue working on this project this is what I'd like to add.
* Support more image types, most specifically Bitmap and JPEG.
* Make supported image type loading dynamic by turning them into plugins
* Add support for changing the image size by changing the width and height in the header.
* Add support for automatic backups of original file.

## Credit
This project uses the following projects.

Fody https://github.com/Fody/Fody and the Fody PropertyChanged add-in https://github.com/Fody/PropertyChanged to trivialize implementing INotifyPropertyChanged

Material Design in Xaml Toolkit https://github.com/ButchersBoy/MaterialDesignInXamlToolkit to make my UI pretty with minimal effort on my part.

