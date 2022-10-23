# Ziedden.ZConnect für .Net
## Beschreibung
Mit dieser Libary können sie Klassen über das HTTP/HTTPS Protokoll Versenden und verarbeiten.

## Benötigt
- EmbedIO
- Newtonsoft.Json

# Infos
## EmbedIO
Eine WebServer Libary vomit die REST Schnittstelle realisiert wurde.

# HowTo Server

## Server bereitstellen
Sie müssen den Server erst bereitstellen. Das machen sie wie Folgt:

Sie müssen BindIP und Port angeben.
```csharp
Ziedden.ZConnect.Server ZServer = new Ziedden.ZConnect.Server("127.0.0.1", 5586);
ZServer.Start();
```

## Packete verarbeiten
Um vom Client gesendete Packete zu verarbeiten müssen sie Funktionen ersetellen und mit Attributen versehen. 

Wo diese erstellt werden ist egal. Sie müssen nur static sein. Die Libary scannt das ganze Asembly ab und sicht diese beim Start.
```csharp
[Ziedden.ZConnect.ServerDatas.Server("TestPacket")]
public static object CalculateTestPacket(object Packet)
{
  .........
  return SomeDatas;
}
```

## PacketParse
Sie können gesendete Packet ganz einfach wieder zu eine Klasse Convertieren. Das geht wie folgt. (BSP)
```csharp
[Ziedden.ZConnect.ServerDatas.Server("TestPacket")]
public static object CalculateTestPacket(object Packet)
{
  TestPacketA PacketA = Ziedden.ZConnect.Server.Parse<TestPacketA>(Packet);
  TestPacketB tpb = new TestPacketB();
  tpb.A = PacketA.A;
  tpb.B = PacketA.B;
  tpb.C = PacketA.A + PacketA.B;
  return tpb;
}
```

# HowTo Client

## Client erstellen
Ein Client muss wie bei jeder verbindung erstellt werden. Anzugeben sind IP und Port
```csharp
Ziedden.ZConnect.Client client = new Ziedden.ZConnect.Client("127.0.0.1", 5586);
```

## SendPacket
Mit SendPacket kann man die Daten zum Server senden und dieser gibt denn die passenden Antwort. (Bezogen auf BSP Server)
als T (zwischen den <>) wurd der Rückgabewert angeben. Als PacketName kommt in Attribut definierte Namen vom Server und als letztes einfach die Klasse die man senden will.
```csharp
TestPacketA tpa = new TestPacketA();
tpa.A = 6;
tpa.B = 10;
Ziedden.ZConnect.Client client = new Ziedden.ZConnect.Client("127.0.0.1", 5586);

/*TestPacketB tpb = client.SendPacket<[RETURN CLASS FOR CONVERTER]>([PACKET NAME], [Send CLASS]);*/
TestPacketB tpb = client.SendPacket<TestPacketB>("TestPacket", tpa);
```

## SendPacketEncrype
Genauso wie SendPacket bloß verschlüsselt.
```csharp
TestPacketA tpa = new TestPacketA();
tpa.A = 6;
tpa.B = 10;
Ziedden.ZConnect.Client client = new Ziedden.ZConnect.Client("127.0.0.1", 5586);

/*TestPacketB tpb = client.SendPacketEncrype<[RETURN CLASS FOR CONVERTER]>([PACKET NAME], [Send CLASS]);*/
TestPacketB tpb = client.SendPacketEncrype<TestPacketB>("TestPacket", tpa);
```
