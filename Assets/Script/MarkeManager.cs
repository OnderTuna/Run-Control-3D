using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onder;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;
using UnityEngine.SceneManagement;

public class MarkeManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_storeController;
    private static IExtensionProvider m_ExtensionProvider;

    private static string Puan_250 = "puan250";
    private static string Puan_500 = "puan500";
    private static string Puan_750 = "puan750";
    private static string Puan_1000 = "puan1000";

    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public Text TextObjeleri;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();

    void Start()
    {
        _VeriYonetimi.DilLoad(); //Veri oku.
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeAktar(); //Okudugumuz verileri aktar.
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[3]);
        DilTercihiYonetim();

        if(m_storeController == null) //Store olusturduk mu.
        {
            InitializePurchasing();
        }
    }

    public void DilTercihiYonetim()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR") /*Hangi dil seceneginin o an aktif olduguna bakip, ona gore dil ayarlarini yap.*/
        {
            TextObjeleri.text = _DilVerileriAnaObje[0]._DilVerileri_TR[0].Metin;
        }
        else
        {
            TextObjeleri.text = _DilVerileriAnaObje[0]._DilVerileri_EN[0].Metin;
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_250, StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") + 250);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_500, StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") + 500);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_750, StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") + 750);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_1000, StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") + 1000);
        }
        return PurchaseProcessingResult.Complete;
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Satýn Alma Baþarýsýz");
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_storeController = controller;
        m_ExtensionProvider = extensions;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void InitializePurchasing()
    {
        if(IsInitalized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Puan_250, ProductType.Consumable);
        builder.AddProduct(Puan_500, ProductType.Consumable);
        builder.AddProduct(Puan_750, ProductType.Consumable);
        builder.AddProduct(Puan_1000, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public void UrunAl_250()
    {
        BuyProductID(Puan_250);
    }
    public void UrunAl_500()
    {
        BuyProductID(Puan_500);
    }
    public void UrunAl_750()
    {
        BuyProductID(Puan_750);
    }
    public void UrunAl_1000()
    {
        BuyProductID(Puan_1000);
    }

    void BuyProductID(string productId)
    {
        if (IsInitalized())
        {
            Product product = m_storeController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                m_storeController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Satýn alýrken hata oluþtu");
            }
        }
        else
        {
            Debug.Log("Ürün çaðýrýlamýyor.");
        }
    }

    private bool IsInitalized()
    {
        return m_storeController != null && m_ExtensionProvider != null;
    }

    public void GeriDon()
    {
        SceneManager.LoadScene(0);
    }
}
