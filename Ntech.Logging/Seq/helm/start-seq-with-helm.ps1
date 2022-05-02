# add the datalust Helm repository
helm repo add datalust https://helm.datalust.co
helm repo update

# install the seq chart
helm install my-seq datalust/seq

# configures ingress for the UI [not working locally]
#helm upgrade --install -f config.yaml my-seq datalust/seq

# create ingress for ui and injection
kubectl apply -f .\ingress-config.yaml