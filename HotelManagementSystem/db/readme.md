docker-compose up -d

clickhouse-client --query "CREATE TABLE booking_request (IdEvent UUID, StatusBooking String, ArrivalDate String, DepartureDate String, NumberOfGuests String, EventCreated UInt64) ENGINE = Kafka('broker:19092', 'Booking', 'consumer-group-2', 'JSONEachRow');"

clickhouse-client --query "CREATE TABLE daily ( day Date, total UInt64 ) ENGINE = SummingMergeTree() ORDER BY (day);"

clickhouse-client --query "CREATE MATERIALIZED VIEW consumer TO daily AS SELECT toDate(toDateTime(EventCreated)) AS day, count() as total FROM default.booking_request GROUP BY day;"
