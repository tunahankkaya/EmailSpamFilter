import pandas as pd

# Load the dataset
df = pd.read_csv('spam_ham_dataset.csv')

# Convert to JSON (list of objects)
json_path = 'spam_data.json'
df.to_json(json_path, orient='records', indent=4)

print(f"JSON file created at {json_path}")
print(df.head(1).to_json(orient='records', indent=4))