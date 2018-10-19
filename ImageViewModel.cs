using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RayTracer {
    public class ImageViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        private WriteableBitmap image;
        
        private bool highRes = true;

        private int width = 600;
        private int height = 300;

        private ImageModel model;

        public ImageViewModel() {
            if (highRes) {
                this.width = 2560;
                this.height = 1440;
            }
            this.Image = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);
            this.model = new ImageModel(this.width, this.height, UpdateImage, this.highRes);
            // Update model.
            this.model.Update();
        }

        public WriteableBitmap Image {
            get => this.image;
            set {
                this.image = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?redirectedfrom=MSDN&view=netframework-4.7.2
        /// </summary>
        public void UpdateImage() {
            try {
                // Reserve the back buffer for updates.
                this.Image.Lock();
                unsafe {
                    
                    for (int row = 0; row < this.height; row++) {
                        for (int col = 0; col < this.width; col++) {
                            // Get a pointer to the back buffer.
                            int pBackBuffer = (int)this.Image.BackBuffer;

                            // Find the address of the pixel to draw.
                            pBackBuffer += (this.height - row - 1) * this.Image.BackBufferStride;
                            pBackBuffer += (col) * 4;

                            // Compute the pixel's color.
                            int color_data = this.model.R[col, row] << 16; // R
                            color_data |= this.model.G[col, row] << 8;   // G
                            color_data |= this.model.B[col, row] << 0;   // B

                            // Assign the color data to the pixel.
                            *((int*)pBackBuffer) = color_data;
                        }
                    }
                    // Specify the area of the bitmap that changed.
                    this.Image.AddDirtyRect(new Int32Rect(0, 0, this.width, this.height));
                } 
            } finally {
                this.Image.Unlock();
            }
            this.Image = this.Image;
            if (this.highRes) {
                SaveFile("result.png", this.Image.Clone());
            }
        }
       
        public void SaveFile(string filename, BitmapSource bitmapSource) {
            if (filename != string.Empty) {
                using (FileStream stream5 = new FileStream(filename, FileMode.Create)) {
                    PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                    encoder5.Frames.Add(BitmapFrame.Create(bitmapSource));
                    encoder5.Save(stream5);
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            if (PropertyChanged != null) {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
    }
}
