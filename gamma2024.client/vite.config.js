import { fileURLToPath, URL } from "node:url";

import { defineConfig } from "vite";
import plugin from "@vitejs/plugin-vue";
import fs from "fs";
import path from "path";
//import child_process from "child_process";

//const baseFolder =
//    process.env.APPDATA !== undefined && process.env.APPDATA !== ""
//        ? `${process.env.APPDATA}/ASP.NET/https`
//        : `${process.env.HOME}/.aspnet/https`;

//const certificateArg = process.argv
//    .map((arg) => arg.match(/--name=(?<value>.+)/i))
//    .filter(Boolean)[0];
//const certificateName = certificateArg
//    ? certificateArg.groups.value
//    : "gamma2024.client";

//if (!certificateName) {
//    console.error(
//        "Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly."
//    );
//    process.exit(-1);
//}

//const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
//const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

//if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
//    if (
//        0 !==
//        child_process.spawnSync(
//            "dotnet",
//            [
//                "dev-certs",
//                "https",
//                "--export-path",
//                certFilePath,
//                "--format",
//                "Pem",
//                "--no-password",
//            ],
//            { stdio: "inherit" }
//        ).status
//    ) {
//        throw new Error("Could not create certificate.");
//    }
//}

// https://vitejs.dev/config/
export default defineConfig((/*{ mode }*/) => {


    // Load env file based on `mode` in the current working directory.
    // Set the third parameter to '' will load all env regardless of the `VITE_` prefix.
    //const env = loadEnv(mode, process.cwd(), '');

    //console.log(env);

    //console.log("env.vite_base_url:" + env.VITE_BASE_URL);
    //console.log("env.vite_api_url:" + env.VITE_API_URL);
    //console.log("NODE_ENV: " + env.NODE_ENV);
    //console.log("Mode: " + mode);
    return {
        plugins: [plugin()],
        resolve: {
            alias: {
                '@': fileURLToPath(new URL('./src', import.meta.url))
            }
        },
        //base: --injected int the npm script
        build: {
            sourcemap: true
        }
    }
})