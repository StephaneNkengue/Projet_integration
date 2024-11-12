import * as signalR from "@microsoft/signalr";

let connection = null;

export const startSignalRConnection = async (baseUrl) => {
  try {
    connection = new signalR.HubConnectionBuilder()
      .withUrl(`${baseUrl}/api/hub/lotMiseHub`)
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

export const getConnection = () => connection;