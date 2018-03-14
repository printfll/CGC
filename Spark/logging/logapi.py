import logging, logging.config
import globalV


class Log:

    def __init__(self):
        self.logger = logging.getLogger(globalV.LOGNAME)
        self.formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
        self.logger.setLevel(logging.WARNING)

    def get_logger(self):
        return self.logger

    def set_level(self, level):
        self.logger.setLevel(level)

    def add_FileHandler(self, file_name, level=logging.INFO):
        handler = logging.FileHandler(file_name)
        handler.setLevel(level)
        handler.setFormatter(self.formatter)
        self.logger.addHandler(handler)

    def add_StreamHandler(self, level=logging.INFO):
        ch = logging.StreamHandler()
        ch.setLevel(level)
        ch.setFormatter(self.formatter)
        self.logger.addHandler(ch)

    def add_Handler(self, handler, level=logging.INFO):
        handler.setLevel(level)
        handler.setFormatter(self.formatter)
        self.logger.addHandler(handler)

    def warning(self, ms):
        self.logger.warning(ms)

    def info(self, ms):
        self.logger.info(ms)

    def debug(self, ms):
        self.logger.debug(ms)

    def error(self, ms):
        self.logger.error(ms)

    def critical(self, ms):
        self.logger.critical(ms)

    def exception(self, ms):
        self.logger.exception(ms)