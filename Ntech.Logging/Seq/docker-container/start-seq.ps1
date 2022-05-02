# mkdir -p local_path_to_store_data

docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -v local-path-to-store-data:/data -p 8081:80 -p 5341:5341 datalust/seq:latest

  #-e SEQ_FIRSTRUN_ADMINPASSWORDHASH="$PH" `