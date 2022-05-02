# add the elastic Helm repository
helm repo add elastic https://helm.elastic.co
helm repo update


# install elasticsearch
helm install elasticsearch elastic/elasticsearch

# install kibana
helm install kibana elastic/kibana