mkdir -p aes
touch ./aes/aes-key.txt

echo "🔑 Генерация AES-256 ключа (32 байта)..."
openssl rand -base64 32 > ./aes/aes-key.txt

echo "🔑 Генерация IV (16 байт)..."
IV=$(openssl rand -base64 16)
echo $IV >> ./aes/aes-key.txt
