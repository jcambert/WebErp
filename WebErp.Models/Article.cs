using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace WebErp.Models
{
    public class Article:ModelBase
    {
        

        /// <summary>
        /// Libelle Article
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Type Article
        /// </summary>
        public TypeArticle Type { get; set; }

        /// <summary>
        /// Matiere de l'article
        /// Peut etre null si article non physique
        /// </summary>
        public Matiere Matiere { get; set; } = null;

        /// <summary>
        /// Article gerer en stock ou non
        /// </summary>
        public bool? GererEnstock { get; set; } = null;

        /// <summary>
        /// Article Fantome
        /// C'est un article qui s'insere dans un autre de manière transparente
        /// </summary>
        public bool? ArticleFantome { get; set; } = null;

        /// <summary>
        /// Autorise les stocks négatifs si l'article est gérer en stock
        /// </summary>
        public bool? AutoriseStockNegatif { get; set; } = null;

        /// <summary>
        /// Autorise la gestion des lots
        /// </summary>
        public bool? GestionParLot { get; set; } = null;

        /// <summary>
        /// Methode d'epuisement si article gerer par lot
        /// </summary>
        public ModeEpuisement? ModeEuipsement { get; set; } = null;

        /// <summary>
        /// Stock Mini autorisé pour le calcul de besoin
        /// Si article gerer en stock
        /// </summary>
        public int? StockMini { get; set; } = null;

        /// <summary>
        /// Stock maxi autorisé pour le calcul des besoins
        /// Si article gerer en stock
        /// </summary>
        public int? StockMaxi { get; set; } = null;

        /// <summary>
        /// Quantite mini de réapprovisionnement pour une quantité economique
        /// si article physique
        /// </summary>
        public int? QuantiteMiniReappro { get; set; } = null;

        /// <summary>
        /// Coeficient de quantité de reapprovisionnemnt
        /// Si article physique
        /// </summary>
        public int? QuantiteLotReappro { get; set; } = null;

        /// <summary>
        /// Stock physique courant
        /// Si article gérer en stock
        /// </summary>
        public int? StockPhysique { get; set; } = null;

        /// <summary>
        /// Stock en commande client
        /// si article gerer en stock
        /// </summary>
        public int? StockReservee { get; set; } = null;

        /// <summary>
        /// Stock en attente de reception sur commande fournisseur
        ///  si article gerer en stock
        /// </summary>
        public int? StockAttendu { get; set; } = null;

        /// <summary>
        /// Stock disponible si article gerer en stock
        /// </summary>
        public int? StockDisponible => StockPhysique - StockReservee;

        /// <summary>
        /// Couts pour la derniere fabrication
        /// </summary>
        //public IList<Cout> Couts { get; set; } = null;

        /// <summary>
        /// Format de tole si article tole
        /// </summary>
        //public Format Format { get; set; } = null;

        /// <summary>
        /// Masse lineaire si article profile
        /// </summary>
        public MassLinear MassLinear { get; set; } = null;

        /// <summary>
        /// Aire lineaire si article profile
        /// </summary>
        public AreaLinear AreaLinear { get; set; } = null;

        /// <summary>
        /// Aire massique si article profile
        /// </summary>
        public AreaMass AreaMass { get; set; } = null;

        /// <summary>
        /// Prix au kilo peut venir du dernier achat!?
        /// </summary>
        public MassCurrency MassCurrency { get; set; } = null;

        /// <summary>
        /// Cout lineaire si article profile
        /// </summary>
        public Currency CostLinear => MassLinear* MassCurrency;

        /// <summary>
        /// Mouvement de stock 
        /// si article physique
        /// si article gerer en stock
        /// </summary>
        //public MouvementsStocks MouvementsStocks { get; set; } = null;

        /// <summary>
        /// Besoins soit de production soit d'achat
        /// </summary>
        //public Besoins Besoins { get; set; } = null;

        /// <summary>
        /// Nomenclature de production
        /// si article fabrique
        /// </summary>
        //public Nomenclatures Nomenclatures { get; set; } = null;

        /// <summary>
        /// Numero de nomenclature active
        /// </summary>
        //public int? NomenclatureVersion { get; set; } = null;

        /// <summary>
        /// Tarif de ventes ou d'achat
        /// </summary>
        //public Tarifs Tarifs { get; set; } = null;

        /// <summary>
        /// Liste des productions si article fabriqué
        /// </summary>
        //public Productions Productions { get; set; } = null;
    }
}

