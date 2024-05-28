#include <windows.h>
#include <winhttp.h>
#include <iostream>

#pragma comment(lib, "winhttp.lib")

class SensorSender {
public:
    void SendRequest(const std::wstring& serverUrl) {
        // Initialize WinHTTP session
        HINTERNET hSession = WinHttpOpen(L"AroundTheWorld Sensor Client",
            WINHTTP_ACCESS_TYPE_DEFAULT_PROXY,
            WINHTTP_NO_PROXY_NAME,
            WINHTTP_NO_PROXY_BYPASS,
            0);
        if (hSession) {
            // Initialize WinHTTP connection
            HINTERNET hConnect = WinHttpConnect(hSession, serverUrl.c_str(), INTERNET_DEFAULT_HTTP_PORT, 0);
            if (hConnect) {
                // Create request handle
                HINTERNET hRequest = WinHttpOpenRequest(hConnect, L"POST", L"/api/Sensor/Update", NULL, WINHTTP_NO_REFERER, WINHTTP_DEFAULT_ACCEPT_TYPES, 0);
                if (hRequest) {
                    // Send the request
                    if (WinHttpSendRequest(hRequest, WINHTTP_NO_ADDITIONAL_HEADERS, 0, WINHTTP_NO_REQUEST_DATA, 0, 0, 0)) {
                        if (WinHttpReceiveResponse(hRequest, NULL)) {
                            // Read response
                            DWORD dwSize = 0;
                            DWORD dwDownloaded = 0;
                            LPSTR pszOutBuffer;
                            BOOL  bResults = FALSE;

                            do {
                                // Keep checking for data until there is nothing left.
                                dwSize = 0;
                                if (!WinHttpQueryDataAvailable(hRequest, &dwSize)) {
                                    printf("Error %u in WinHttpQueryDataAvailable.\n", GetLastError());
                                }

                                // Allocate memory for the buffer.
                                pszOutBuffer = new char[dwSize + 1];
                                if (!pszOutBuffer) {
                                    printf("Out of memory\n");
                                    dwSize = 0;
                                }
                                else {
                                    // Read the data.
                                    ZeroMemory(pszOutBuffer, dwSize + 1);

                                    if (!WinHttpReadData(hRequest, (LPVOID)pszOutBuffer, dwSize, &dwDownloaded)) {
                                        printf("Error %u in WinHttpReadData.\n", GetLastError());
                                    }
                                    else {
                                        // Print data.
                                        printf("%s", pszOutBuffer);
                                    }

                                    // Free the memory allocated to the buffer.
                                    delete[] pszOutBuffer;
                                }
                            } while (dwSize > 0);
                        }
                    }
                    WinHttpCloseHandle(hRequest);
                }
                WinHttpCloseHandle(hConnect);
            }
            WinHttpCloseHandle(hSession);
        }
    }

    void StartSending(const std::wstring& serverUrl, int intervalInSeconds) {
        while (true) {
            SendRequest(serverUrl);
            Sleep(intervalInSeconds * 1000); // Convert seconds to milliseconds
        }
    }
};

int main() {
    SensorSender sender;
    std::wstring serverUrl = L"https://localhost:7160";
    int intervalInSeconds = 30;

    sender.StartSending(serverUrl, intervalInSeconds);

    return 0;
}
