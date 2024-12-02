var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("orleans-redis")
    .WithLifetime(ContainerLifetime.Session)
    .WithRedisInsight();

var orleans = builder.AddOrleans("orleans-cluster")
    .WithClustering(redis)
    .WithGrainStorage("grain-storage", redis);

var api = builder.AddProject<Projects.OrleansBlogPosts_Api>("orleans-api")
    .WithReference(orleans)
    .WaitFor(redis)
    .WithReplicas(1);

builder.Build().Run();
