#include <SoftwareSerial.h>
#include <TinyGPS++.h>

const char* server = "https://localhost:7160/RentItem/Position";
const int port = 80;

#define RX_PIN 4
#define TX_PIN 3
#define GSM_RX 7
#define GSM_TX 8

class GPSSensor {
public:
    GPSSensor(int rxPin, int txPin, int gsmRxPin, int gsmTxPin)
        : gpsSerial(rxPin, txPin), gsmSerial(gsmRxPin, gsmTxPin) {}

    void begin() {
        Serial.begin(9600);
        gpsSerial.begin(9600);
        gsmSerial.begin(9600);

        gsmSerial.println("AT");
        delay(1000);
        gsmSerial.println("AT+CPIN?");
        delay(1000);
        gsmSerial.println("AT+CREG?");
        delay(1000);
        gsmSerial.println("AT+CGATT?");
        delay(1000);
        gsmSerial.println("AT+CIPSHUT");
        delay(1000);
        gsmSerial.println("AT+CIPMUX=0");
        delay(1000);
        gsmSerial.println("AT+CSTT=\"Vladsh123\",\"Vladsh123\",\"QWEqwe123.\"");
        delay(1000);
        gsmSerial.println("AT+CIICR");
        delay(1000);
        gsmSerial.println("AT+CIFSR");
        delay(1000);
    }

    void update() {
        while (gpsSerial.available() > 0) {
            gps.encode(gpsSerial.read());
        }

        if (gps.location.isUpdated()) {
            float latitude = gps.location.lat();
            float longitude = gps.location.lng();

            Serial.print("Latitude: ");
            Serial.println(latitude, 6);
            Serial.print("Longitude: ");
            Serial.println(longitude, 6);

            sendLocation(latitude, longitude);

            delay(30000);
        }
    }

private:
    SoftwareSerial gpsSerial;
    SoftwareSerial gsmSerial;
    TinyGPSPlus gps;

    void sendLocation(float latitude, float longitude) {
        if (gsmSerial.available()) {
            gsmSerial.println("AT+CIPSTART=\"TCP\",\"" + String(server) + "\"," + String(port));
            delay(5000);

            if (gsmSerial.find("CONNECT OK")) {
                String postData = "Id=c5217b8a-358c-4511-9139-ea263ac0ec08&Latitude=" + String(latitude, 6) + "&Longitude=" + String(longitude, 6) + "&Timestamp=" + String(millis());

                gsmSerial.print("AT+CIPSEND=");
                gsmSerial.println(postData.length());
                delay(100);
                gsmSerial.print(postData);
                delay(100);
                gsmSerial.write(0x1A); 
                delay(5000);

                gsmSerial.println("AT+CIPCLOSE"); 
                delay(1000);
            }
            else {
                Serial.println("Connection failed");
            }
        }
    }
};

GPSSensor sensor(RX_PIN, TX_PIN, GSM_RX, GSM_TX);

void setup() {
    sensor.begin();
}

void loop() {
    sensor.update();
}
