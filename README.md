# Assessment
## Merhaba

Bu proje Bir çok mikro servisin  olduğu mimaride olayları alıp kafka üzerinden olarların dağırılmasına tönelik haırlanmış bir örnektir.


### API
3 adet micro servisimiz projemizde yer almakatır. bu mikro servisler aynı veri ve api şemasını kullanmaktadır.


#### POST / - Event İletme

Request:

```http
POST /
Content-Type: application/json

{
    "events": [
        {
            "app": "1231232-321312-12312321-21312",
            "type": "HOTEL_CREATE",
            "time": "2020-02-10T13:40:27.650Z",
            "isSucceeded": true,
            "meta": {
            },
            "user": {
                "isAuthenticated": true,
                "provider": "b2c-internal",
                "id": 231213,
                "e-mail": "eser.ozvataf@setur.com"
            },
            "attributes": {
                "hotelId": 4123,
                "hotelRegion": "Antalya",
                "hotelName": "Rixos"
            }
        }
    ]
}
```

Response:

```http
HTTP 200 OK
```

Alanlar:

|Alan                 |Açıklama                                                 |Tip      |Zorunluluk |
|---------------------|---------------------------------------------------------|---------|-----------|
|app                  |Guid cinsinden uygulamanın kimliği                       |Guid     |Evet       |
|type                 |Event/olay kategorisi                                    |Enum     |Evet       |
|time                 |Olayın gerçekleştiği zaman                               |DateTime |Evet       |
|isSucceeded          |İlgili olay başarıyla sonlanmış mı?                      |Boolean  |Evet       |
|meta.*               |Olaya ait ek detaylar                                    |Object   |Hayır      |
|user.isAuthenticated |Olayı gerçekleştiren kullanıcı giriş yapmış mı?          |Boolean  |Evet       |
|user.provider        |Olayı gerçekleştiren kullanıcının kaydı hangi sistemde?  |String   |Evet       |
|user.id              |Olayı gerçekleştiren kullanıcının id’si                  |Any?     |Evet       |
|user.e-mail          |Olayı gerçekleştiren kullanıcının e-mail bilgisi         |String   |Hayır      |
|attributes.*         |Olaya kategorisine özel detaylar                         |Object   |Hayır      |


### Derleme ve Test

#### Derleme

Bu projeyi derlemek ve ayağa kaldırmak için bilgisayarınızda bulunması gereken docker desktop'tır.

Bundan sonra yapmadınız gerekn proje klasörüne komut satırı açmak ve aşağıdaki komutu çalıştırmaktır.

> docker-compose up

#### Test

Projeti test etmek için proje klasörüne komut satırı açmak ve aşağıdaki komutu çalıştırmanız yeterli olacaktır.

> dotnet test AssessmentTest/AssessmentTest.csproj