# SolutionLps

## Test A

### Opinion 1
Bisa di lihat pada file berikut :
```
Solution\LogicalCode\OpinionFirst.cs
```

bentuk sederhanaya pada property yang kemungkinan null bisa di tambahkan dengan tanda ?

### Opinion 2
Bisa di lihat pada file berikut :
```
Solution\LogicalCode\OpinionSecond.cs
```

ada perbaikan untuk declare List object

### Opinion 3
Bisa di lihat pada file berikut :
```
Solution\LogicalCode\OpinionThrid.cs
```

perbaikan ketika pemanggilan variable kelas Laptop

### MemoryLeak 1
Bisa di lihat pada file berikut :
```
Solution\LogicalCode\MemoryLeakFirst.cs
```

perbaikannya hampir sama yaitu pembuatan variable List object

### MemoryLeak 2
Bisa di lihat pada file berikut :
```
Solution\LogicalCode\MemoryLeakSecond.cs
```

kurang pembuatan object delegate, dan pemanggialn publishernya harusnya tidak perlu di jadikan parameter consturctor

### MemoryLeak 3
Bisa di lihat pada file berikut :
```
Solution\LogicalCode\MemoryLeakThrid.cs
```

tidak ada function yang aneh, ini harusnya bisa berjalan jika di running

### MemoryLeak 4
Bisa di lihat pada file berikut :
```
Solution\LogicalCode\MemoryLeakFourth.cs
```

Console.ReadLine() harus di simpan sebagai inputan untuk mengaktifkan function Cache.Get untuk mengambil object dari list berdasarkan index



## Test B

### Minio

untuk object storage yang sering saya gunakan pada project2 sebelumnya, untuk demo kali ini saya menggunakan ip public milik pribadi saya, jika mau lihat bisa cek link sbb :

http://103.171.164.79:34001/login
username : userlps
password : adminlps123

### How to run WebApp

#### 1. Update Config appsettings.json

ganti koneksi server yang di inginkan
```
"ConnectionStrings": {
  "PostgreSQLConnection": "Host=xxxx;Database=xxxx;Username=xxxx;Password=xxxx"
},
```

#### 2. buat database sesuai nama yang di tentukan pada connection di atas
```
  "PostgreSQLConnection": "Host=xxxx;Database=xxxx;Username=xxxx;Password=xxxx"
```

#### 3. buka terminal Package Manager Console
Click View -> Other Windows -> Package Manager Console

#### 4. running script berikut pada terminal Package Console
```
add-migration initial-context
```

akan muncul di layar console sbb:
```
PM> add-migration init
Build started...
Build succeeded.
To undo this action, use Remove-Migration.
```

maka akan terbentuk folder dan file migration

#### 5. Jika Build success, maka update database dengan perintah berikut
```
update-database
```


