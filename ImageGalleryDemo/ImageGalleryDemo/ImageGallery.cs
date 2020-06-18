using C1.Win.C1Tile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGalleryDemo
{
    public partial class ImageGallery : Form
    {
        DataFetcher datafetch = new DataFetcher();
        List<ImageItem> imagesList;
        int checkedItems = 0;

        public ImageGallery()
        {
            InitializeComponent();
        }

        private void AddTiles(List<ImageItem> imageList)
        {
            _imageTileControl.Groups[0].Tiles.Clear();
            foreach (var imageitem in imageList)
            {
                Tile tile = new Tile();
                tile.HorizontalSize = 2;
                tile.VerticalSize = 2;
                _imageTileControl.Groups[0].Tiles.Add(tile);
                Image img = Image.FromStream(new
               MemoryStream(imageitem.Base64));
                Template tl = new Template();
                ImageElement ie = new ImageElement();
                ie.ImageLayout = ForeImageLayout.Stretch;
                tl.Elements.Add(ie);
                tile.Template = tl;
                tile.Image = img;
            }
        }


        private async Task Search_ClickAsync(object sender, EventArgs e)
        {
          //  statusStrip1.Visible = true;
            imagesList = await datafetch.GetImageData(_searchBox.Text);
            AddTiles(imagesList);
           // statusStrip1.Visible = false;

        }


        private void OnTileChecked(object sender, C1.Win.C1Tile.TileEventArgs e)
        {
            checkedItems++;
           // _exportImage.Visible = true;
        }

        private void OnTileUnchecked(object sender, TileEventArgs e)
        {
            checkedItems--;
            //_exportImage.Visible = checkedItems > 0;
        }


        private void OnSearchPanelPaint(object sender, PaintEventArgs e)
        {
            Rectangle r = _searchBox.Bounds;
            r.Inflate(5, 5);
            Pen p = new Pen(Color.Blue);
            e.Graphics.DrawRectangle(p, r);
        }



    }
}
