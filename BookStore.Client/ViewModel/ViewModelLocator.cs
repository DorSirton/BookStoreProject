

using BookStore.Client.Services;
using BookStore.Server;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;


namespace BookStore.Client.ViewModel
{
   
    public class ViewModelLocator
    {
     
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginPageViewModel>();
            SimpleIoc.Default.Register<MenegerOptionViewModel>();
            SimpleIoc.Default.Register<CostumerOptionViewModel>();
            SimpleIoc.Default.Register<ReportsViewModel>();
            SimpleIoc.Default.Register<RemoveSaleViewModel>();
            SimpleIoc.Default.Register<NewSaleViewModel>();
            SimpleIoc.Default.Register<RemoveFromStockViewModel>();
            SimpleIoc.Default.Register<AddToStockViewModel>();
            SimpleIoc.Default.Register<ProdactListViewModel>();
            SimpleIoc.Default.Register<BuyingProdactViewModel>();
            SimpleIoc.Default.Register<ProdactService>();
            SimpleIoc.Default.Register<RoleService>();
            SimpleIoc.Default.Register<PaymentPageViewModel>();
            SimpleIoc.Default.Register<SaleByProdactTypeViewModel>();
            SimpleIoc.Default.Register<SaleBySpaciphicProdactViewModel>();
            SimpleIoc.Default.Register<SaleByGenereViewModel>();
        }


        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public SaleByGenereViewModel SbgSale => ServiceLocator.Current.GetInstance<SaleByGenereViewModel>();
        public SaleBySpaciphicProdactViewModel SpSale => ServiceLocator.Current.GetInstance<SaleBySpaciphicProdactViewModel>();
        public SaleByProdactTypeViewModel PtSale => ServiceLocator.Current.GetInstance<SaleByProdactTypeViewModel>();
        public PaymentPageViewModel Payment => ServiceLocator.Current.GetInstance<PaymentPageViewModel>();
        public BuyingProdactViewModel Buying => ServiceLocator.Current.GetInstance<BuyingProdactViewModel>();
        public ProdactListViewModel Prodacts => ServiceLocator.Current.GetInstance<ProdactListViewModel>();
        public RemoveFromStockViewModel RemoveFromStock => ServiceLocator.Current.GetInstance<RemoveFromStockViewModel>();
        public AddToStockViewModel AddToStock => ServiceLocator.Current.GetInstance<AddToStockViewModel>();
        public NewSaleViewModel NewSale => ServiceLocator.Current.GetInstance<NewSaleViewModel>();
        public RemoveSaleViewModel RemoveSale => ServiceLocator.Current.GetInstance<RemoveSaleViewModel>();
        public ReportsViewModel Reports => ServiceLocator.Current.GetInstance<ReportsViewModel>();
        public LoginPageViewModel LoginPage => ServiceLocator.Current.GetInstance<LoginPageViewModel>();
        public MenegerOptionViewModel MenegerOptionPage => ServiceLocator.Current.GetInstance<MenegerOptionViewModel>();
        public CostumerOptionViewModel CostumerOptionPage => ServiceLocator.Current.GetInstance<CostumerOptionViewModel>();







    }
}