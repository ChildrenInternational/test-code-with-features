import os
import csv
import requests
from PIL import Image
from io import BytesIO
from azure.storage.blob import BlobServiceClient, BlobClient, ContainerClient

def read_csv_from_blob(blob_service_client, container_name, blob_name):
    blob_client = blob_service_client.get_blob_client(container=container_name, blob=blob_name)
    download_stream = blob_client.download_blob()
    csv_content = download_stream.readall().decode('utf-8')
    return csv.reader(csv_content.splitlines())

def check_image_status(url):
    try:
        response = requests.get(url)
        if response.status_code != 200:
            return 'Missing'
        
        image = Image.open(BytesIO(response.content))
        width, height = image.size
        
        if width > height:
            return 'Rotated'
        elif width == 359 and height == 531:
            return 'Placeholder'
        else:
            return 'Good'
    except Exception as e:
        return 'Error'

def save_results_to_blob(blob_service_client, container_name, blob_name, content):
    blob_client = blob_service_client.get_blob_client(container=container_name, blob=blob_name)
    blob_client.upload_blob(content, overwrite=True)

def main():
    connection_string = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    input_container_name = 'input-container'
    input_blob_name = 'ids.csv'
    output_container_name = 'output-container'
    output_blob_name = 'results.csv'
    aggregate_blob_name = 'aggregate.txt'
    base_url = 'https://example.com/images/'

    blob_service_client = BlobServiceClient.from_connection_string(connection_string)
    csv_reader = read_csv_from_blob(blob_service_client, input_container_name, input_blob_name)

    results = []
    status_count = {'Good': 0, 'Rotated': 0, 'Missing': 0, 'Error': 0}

    for row in csv_reader:
        id = row[0]
        url = f"{base_url}{id}"
        status = check_image_status(url)
        results.append([id, status])
        status_count[status] += 1

    results_csv = '\n'.join([','.join(row) for row in results])
    save_results_to_blob(blob_service_client, output_container_name, output_blob_name, results_csv)

    aggregate_content = '\n'.join([f"{status}: {count}" for status, count in status_count.items()])
    save_results_to_blob(blob_service_client, output_container_name, aggregate_blob_name, aggregate_content)

if __name__ == "__main__":
    main()
