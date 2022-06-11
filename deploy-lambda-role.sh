aws iam create-policy --policy-name lambda-policy --policy-document file://lambda-policy.json
aws iam create-role --role-name lambda-role --assume-role-policy-document file://lambda-role-trust-policy.json
aws iam put-role-policy --role-name lambda-role --policy-name lambda-policy --policy-document file://lambda-policy.json