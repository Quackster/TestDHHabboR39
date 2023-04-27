using Stargazer.Networking.Game;

await Stargazer.Stargazer.Boot();

while (true)
{
    switch (Console.ReadLine())
    {
        case "exit":
            await GameNetworkServer.Instance.Stop();
            Environment.Exit(0);
            break;
    }
}