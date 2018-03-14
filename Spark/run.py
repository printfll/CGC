import sys
sys.path.append("./task")
sys.path.append("./spark")
sys.path.append("./logging")
import server
import globalV
import logapi
import logging


if __name__ == '__main__':
    logger = logapi.Log()
    logger.set_level(logging.INFO)
    logger.add_FileHandler("./logging/log.log")
    logger.add_StreamHandler()
    logger.info("Start spark server")
    server.serve()
    logger.info("Stop spark server")