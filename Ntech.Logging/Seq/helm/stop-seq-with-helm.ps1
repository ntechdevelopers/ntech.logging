# delete ingress for ui and injection
kubectl delete ingress my-seq-ingress

# uninstall the seq chart
helm uninstall my-seq datalust/seq
