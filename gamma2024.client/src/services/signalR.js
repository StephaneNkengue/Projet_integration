import * as signalR from "@microsoft/signalr";

let connection = null;

export const startSignalRConnection = async (baseUrl, token) => {
  if (connection && connection.state === "Connected") {
    console.log("Réutilisation d'une connexion SignalR existante");
    return connection;
  }

  try {
    const cleanBaseUrl = baseUrl.endsWith('/') ? baseUrl.slice(0, -1) : baseUrl;
    
    connection = new signalR.HubConnectionBuilder()
      .withUrl(`${cleanBaseUrl}/api/hub/lotMiseHub`, {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build();

    await connection.start();
    console.log("SignalR Connected.");
    return connection;
  } catch (err) {
    console.error("SignalR Connection Error: ", err);
    return null;
  }
};

export const stopSignalRConnection = async () => {
  if (connection) {
    await connection.stop();
    connection = null;
  }
};

export const getConnection = () => connection;