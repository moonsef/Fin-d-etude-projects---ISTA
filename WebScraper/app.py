import datetime
from bs4 import BeautifulSoup
import requests
import random
import pyodbc
import os



links = []
# I have hidden the site link for copyright reasons...
url = ""

etats = ["neuf", "occasion"]
states = ["en stock", "rupture de stock"]

try:
    conn = pyodbc.connect('Driver={SQL Server};'
                        'Server=DESKTOP-MMR96DC;'
                        'Database=db;'
                        'Trusted_Connection=yes;')

    cursor = conn.cursor()
except Exception as e:
    print(e)
    exit(0)


def image_url_to_binray(link):
    return requests.get(link).content

def random_datetime():
    start_date = datetime.datetime(2019, 1, 1)
    end_date = datetime.datetime.now()
    time_between_dates = end_date - start_date
    days_between_dates = time_between_dates.days
    random_number_of_days = random.randrange(days_between_dates)
    return start_date + datetime.timedelta(days=random_number_of_days,minutes=random.randint(0,59), seconds=random.randint(0,59))


def query_links():
    print("getting links...")
    for i in range(1, 2):
        res = requests.get(url.format(i))
        soup = BeautifulSoup(res.content, features="html.parser")
        articles = soup.find_all("article", {"class": "product-miniature"})
        for article in articles:
            link = article.find("a", {"class": "product-thumbnail"})
            links.append(link.attrs["href"])
        print("page", i , "copied")
    os.system("clear")


def load_data():
    print("loading data...\n")
    for i, link in enumerate(links):
        if i > 3 and i < 30:
            res = requests.get(link)
            soup = BeautifulSoup(res.content, features="html.parser")
            brand_dom = soup.find("div", {"class": "product-manufacturer"})
            if brand_dom != None:
                brand_name = brand_dom.select("a img")[0].get("alt")
    

                categorie = link.split("/")[3]
                produit_name = soup.find("h1" , {"class": "product-title"}).text.strip()
                produit_prix = soup.find("span", {"class": "price"}).text.strip().replace("\xa0", "").replace(",00", "").replace("MAD", "")
                produit_desc = soup.find("div", {"class": "product-description-short"}).text.strip()
                produit_state = states[random.randint(0,1)]
                produit_etat = etats[random.randint(0,1)]
                if produit_state != "rupture de stock":
                    quantity = random.randint(50,100)
                else:
                    quantity = 0

             

                print("link number", i, "finished loading data. inserting to db...")
                insert_to_db(categorie,produit_name, produit_prix, produit_desc, produit_state, produit_etat, quantity,soup.find_all("img", {"class": "thumb"}), brand_name, brand_dom.select("a img")[0].get("src"))



def insert_to_db(categorie,produit_name, produit_prix, produit_desc, produit_state, produit_etat, quantity,produit_photos, brand_name, brand_photo):
    cursor.execute(
        """
            SELECT id FROM categories WHERE nom=?  
        """,
        categorie
    )
    categorie_id = None
    marque_id = None

    row_categorie = cursor.fetchone() 
    if row_categorie != None:
        categorie_id = row_categorie.id
        print("categorie already exists.")    
    else:
        cursor.execute(
            """ 
                INSERT INTO categories
                VALUES (?);
            """,
            categorie
        )
        cursor.commit()
        cursor.execute("SELECT @@IDENTITY AS id;")
        categorie_id = cursor.fetchone().id
        print("categorie inserted.")    
    

    
    cursor.execute(
        """
            SELECT id FROM marques WHERE nom=?  
        """,
        brand_name
    )
    row_marque = cursor.fetchone() 
    if row_marque != None:
            marque_id = row_marque.id
            print("marque already exists.")    
    else:
        cursor.execute(
            """ 
                INSERT INTO marques
                VALUES (?,?);
            """,
            
            brand_name,
            image_url_to_binray(brand_photo)
        )
        cursor.commit()
        cursor.execute("SELECT @@IDENTITY AS id;")
        marque_id = cursor.fetchone().id
        print("marque inserted.")  


    cursor.execute(
        """ 
            INSERT INTO produits
            VALUES (?,?,?,?,?,?,?,?,?);
        """,
        produit_name,
        produit_prix,
        produit_desc,
        quantity,
        produit_etat,
        produit_state,
        random_datetime(),
        categorie_id,
        marque_id
    )
    cursor.commit()
    cursor.execute("SELECT @@IDENTITY AS id;")
    print("produit inserted.")

    produit_id = cursor.fetchone().id

    for photo in produit_photos:
        cursor.execute(
            """
                INSERT INTO produit_photos
                VALUES (?,?)
            """,
            image_url_to_binray(photo.attrs["src"]),
            produit_id
        )
        cursor.commit()
    
    print("photos inserted.\n")


query_links()
load_data()






