using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shafikov41Size
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;

        List<Product> CurrentPageList = new List<Product>();
        List<Product> TableList;
        public ProductPage()
        {
            InitializeComponent();

            var currentProduct = Shafikov41SizeEntities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProduct;

            ProdAll.Text = Convert.ToString(currentProduct.Count);

            CostComboBox.SelectedIndex = 0;
            DiscntComboBox.SelectedIndex = 0;

            UpdateProduct();
        }

        private void CostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void ProdSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void UpdateProduct()
        {
            var currentProduct = Shafikov41SizeEntities.GetContext().Product.ToList();

            currentProduct = currentProduct.Where(p => p.ProductName.ToLower().Contains(ProdSearch.Text.ToLower())).ToList();

            if (CostComboBox.SelectedIndex == 0)
            {

            }
            else if (CostComboBox.SelectedIndex == 1)
            {
                currentProduct = currentProduct.OrderBy(p => p.ProductCost).ToList();
            }
            else if (CostComboBox.SelectedIndex == 2)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }

            if (DiscntComboBox.SelectedIndex == 0)
            {

            }
            else if(DiscntComboBox.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount < 10)).ToList();
            }
            else if (DiscntComboBox.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15)).ToList();
            }
            else if (DiscntComboBox.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 15)).ToList();
            }         

            ProdAtTheMoment.Text = Convert.ToString(currentProduct.Count);

            ProductListView.ItemsSource = currentProduct;

            TableList = currentProduct;

            //ChangePage(0, 0);
        }

        private void DiscntComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        /*private void LeftSlideBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void RightSlideBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if(CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }

            Boolean Ifupdate = true;

            int min;

            if(selectedPage.HasValue)
            {
                if(selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for(int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch(direction)
                {
                    case 1:
                        if(CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for(int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    case 2:
                        if(CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;

                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;

                            for(int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }

            if(Ifupdate)
            {
                PageListBox.Items.Clear();

                for(int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;

                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();

                ProductListView.ItemsSource = CurrentPageList;
                ProductListView.Items.Refresh();
            }
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }
        */
    }
}
