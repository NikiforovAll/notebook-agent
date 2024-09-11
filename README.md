# Notebook Agent

This project demonstrates how to use Semantic Kernel and Kernel Memory in a notebook environment.

See [playground.ipynb](./src/playground.ipynb) to get started.

<video src="./assets/demo1.mp4" controls="controls"></video>

It set's up a notebook environment with the following components:

- Kernel Memory - configued with OpenAI, pgvector. 
    - See [setup-kernel.ipynb](./src/setup-kernel.ipynb) for the setup code.
    - See [appsettings.json](./src/appsettings.json) for the configuration.
- Infrastructure - `Postgres/pgvector`, `Aspire Dashboard`
    - See [setup-infrastructure.ipynb](./src/setup-infrastructure.ipynb) for the setup code.


## References

- [Semantic Kernel](https://github.com/microsoft/semantic-kernel)
- [Kernel Memory](https://github.com/microsoft/kernel-memory)
- [.NET Interactive](https://github.com/dotnet/interactive)

## Known Issues

* Traces are not being exported to OTLP exporter from notebooks