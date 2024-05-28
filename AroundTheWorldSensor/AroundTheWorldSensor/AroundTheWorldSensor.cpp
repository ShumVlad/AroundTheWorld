#include <SoftwareSerial.h>
#include <TinyGPS++.h>

// Replace with your server details
const char* server = "your_server_address";  // e.g., "192.168.0.10" or "example.com"
const int port = 80;

// Pins for GPS and GSM modules
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

        // Initialize GSM module
        gsmSerial.println("AT"); // Check communication
        delay(1000);
        gsmSerial.println("AT+CPIN?"); // Check SIM status
        delay(1000);
        gsmSerial.println("AT+CREG?"); // Check network registration
        delay(1000);
        gsmSerial.println("AT+CGATT?"); // Check GPRS status
        delay(1000);
        gsmSerial.println("AT+CIPSHUT"); // Reset IP session
        delay(1000);
        gsmSerial.println("AT+CIPMUX=0"); // Single connection mode
        delay(1000);
        gsmSerial.println("AT+CSTT=\"your_apn\",\"your_user\",\"your_pass\""); // Start task and set APN
        delay(1000);
        gsmSerial.println("AT+CIICR"); // Bring up wireless connection
        delay(1000);
        gsmSerial.println("AT+CIFSR"); // Get local IP address
        delay(1000);
    }

    void update() {
        while (gpsSerial.available() > 0) {
            gps.encode(gpsSerial.read());
        }

        if (gps.location.isUpdated()) {
            float latitude = gps.location.lat();
            float longitude = gps.location.lng();

            // Print location to Serial Monitor
            Serial.print("Latitude: ");
            Serial.println(latitude, 6);
            Serial.print("Longitude: ");
            Serial.println(longitude, 6);

            // Send location to server
            sendLocation(latitude, longitude);

            delay(30000); // Delay for 30 seconds
        }
    }

private:
    SoftwareSerial gpsSerial;
    SoftwareSerial gsmSerial;
    TinyGPSPlus gps;

    void sendLocation(float latitude, float longitude) {
        if (gsmSerial.available()) {
            gsmSerial.println("AT+CIPSTART=\"TCP\",\"" + String(server) + "\"," + String(port)); // Start connection
            delay(5000);

            if (gsmSerial.find("CONNECT OK")) {
                String postData = "Id=c5217b8a-358c-4511-9139-ea263ac0ec08&Latitude=" + String(latitude, 6) + "&Longitude=" + String(longitude, 6) + "&Timestamp=" + String(millis());

                gsmSerial.print("AT+CIPSEND="); // Send data length
                gsmSerial.println(postData.length());
                delay(100);
                gsmSerial.print(postData); // Send data
                delay(100);
                gsmSerial.write(0x1A); // End of data
                delay(5000);

                gsmSerial.println("AT+CIPCLOSE"); // Close connection
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
